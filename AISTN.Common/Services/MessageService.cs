using AISTN.Common.Helper;
using AISTN.Common.Models;
using AISTN.Common.Models.PageResult;
using AISTN.Data.DataModel;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace AISTN.Common.Services
{
    [Injectable]
    public class MessageService : ServiceBase
    {
        private readonly IGenericRepository<Message> _messageRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Receiver> _receiverRepository;
        private readonly IGenericRepository<Sender> _senderRepository;
        private readonly IGenericRepository<MessageReceiver> _messageReceiverRepository;
        private readonly DocumentService _documentService;
        private readonly EmailService _emailService;

        public MessageService(IMapper mapper, ExceptionLogger logger,
                              UserService userService,
                              IHttpContextAccessor contextAccessor,
                              IGenericRepository<Message> messageRepository,
                              IGenericRepository<User> userRepository,
                              ExcelGenerator excelGenerator, IGenericRepository<Receiver> receiverRepository, IGenericRepository<Sender> senderRepository, IGenericRepository<MessageReceiver> messageReceiverRepository, DocumentService documentService, EmailService emailService)
                        : base(mapper, logger, excelGenerator)
        {
            SetCurrentUser(userService.GetCurrentUser(contextAccessor.HttpContext!).ResultData);
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _receiverRepository = receiverRepository;
            _senderRepository = senderRepository;
            _messageReceiverRepository = messageReceiverRepository;
            _documentService = documentService;
            _emailService = emailService;
        }

        public OperationResult<DetailsMessageDTO> GetMessageById(Guid id)
        {
            try
            {
                var message = _messageRepository.GetById(id, src => src.Include(x => x.Sender)
                                                                       .Include(x => x.MessageReceivers)
                                                                         .ThenInclude(x => x.Receiver)
                                                                         .ThenInclude(x => x.User)
                                                                       .Include(x => x.Reply)
                                                                       .Include(x => x.Parent));
                var emails = new List<string>();
                var shouldSave = false;
                foreach (var messageReceiver in message.MessageReceivers)
                {
                    
                    if (messageReceiver.Receiver.UserId == _currentUser.UserId && messageReceiver.Seen == false)
                    {
                        messageReceiver.Seen = true;
                        messageReceiver.SeenDate = DateTime.Now;
                        shouldSave = true;
                    }

                    emails.Add(messageReceiver.Receiver.User.Email!);
                }

                message.SyndicElectronicAddress = string.Join(", ", emails);

                if (shouldSave)
                {
                    _messageRepository.Update(message);
                    _messageRepository.Save();
                }

                message.Sender.User = _userRepository.GetById(message.Sender.UserId.Value, src => src.Include(x => x.Roles));

                return Success(_mapper.Map<DetailsMessageDTO>(message));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<DetailsMessageDTO>(ex);
            }
        }

        public OperationResult<int> GetUnreadMessages()
        {
            try
            {
                var messages = _messageReceiverRepository.Get(x => x.Receiver.UserId == _currentUser.UserId && x.Seen == false, src => src.Include(x => x.Message));

                return Success(messages.Count());
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<int>(ex);
            }
        }

        public OperationResult<PagedList<MessageIndexDTO>> GetSentMessages(int pageNumber, int pageSize)
        {
            try
            {
                var senders = _messageReceiverRepository.Get(x => x.Message.Sender.UserId == _currentUser.UserId, src => src.Include(x => x.Message)
                                                                                                                                .ThenInclude(x => x.Sender)
                                                                                                                            .Include(x => x.Receiver))
                                                                                                                            .AsQueryable();

                return Success(PagedList<MessageIndexDTO>.ToPagedList(senders.OrderByDescending(x => x.Message.DateCreated)
                              .ProjectTo<MessageIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<MessageIndexDTO>>(ex);
            }
        }

        public OperationResult<PagedList<MessageIndexDTO>> GetReceivedMessages(int pageNumber, int pageSize)
        {
            try
            {
                var receivers = _messageReceiverRepository.Get(x => x.Receiver.UserId == _currentUser.UserId, 
                                                                                         src => src.Include(x => x.Message)
                                                                                                       .ThenInclude(x => x.Sender)
                                                                                                   .Include(x => x.Message)
                                                                                                       .ThenInclude(x => x.Reply)
                                                                                                   .Include(x => x.Message)
                                                                                                       .ThenInclude(x => x.Parent)
                                                                                                   .Include(x => x.Receiver))
                                                                                                   .AsQueryable();

                return Success(PagedList<MessageIndexDTO>.ToPagedList(receivers.OrderByDescending(x => x.Message.DateCreated)
                              .ProjectTo<MessageIndexDTO>(_mapper.ConfigurationProvider), pageNumber, pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<PagedList<MessageIndexDTO>>(ex);
            }
        }

        public OperationResult<SaveMessageDTO> SendMessage(SaveMessageDTO messageDTO)
        {
            try
            {
                var curuser = _userRepository.GetById(_currentUser.UserId.Value);
                var sender = _senderRepository.Get(x => x.UserId == curuser.Id).FirstOrDefault();

                var message = _mapper.Map<Message>(messageDTO);
                if (sender != null)
                {
                    message.SenderId = sender.Id;
                }
                else
                {
                    message.Sender = new Sender()
                    {
                        User = curuser
                    };
                }

                if (messageDTO.SendToAll == true)
                {
                    var users = _userRepository.GetAll();
                    foreach (var user in users)
                    {
                        var receiver = _receiverRepository.Get(x => x.UserId == user.Id).FirstOrDefault();

                        if (receiver != null)
                        {
                            message.MessageReceivers.Add(new MessageReceiver
                            {
                                Receiver = receiver
                            });
                        }
                        else
                        {
                            message.MessageReceivers.Add(new MessageReceiver
                            {
                                Receiver = new Receiver { UserId = user.Id }
                            });
                        }
                    }
                }
                else
                {
                    foreach (var userId in messageDTO.MessageReceiverIDs!)
                    {
                        var user = _userRepository.GetById(userId);
                        var receiver = _receiverRepository.Get(x => x.UserId == userId).FirstOrDefault();

                        if (receiver != null)
                        {
                            message.MessageReceivers.Add(new MessageReceiver
                            {
                                Receiver = receiver
                            });
                        }
                        else
                        {
                            message.MessageReceivers.Add(new MessageReceiver
                            {
                                Receiver = new Receiver { UserId = userId }
                            });
                        }
                    }
                }

                if (messageDTO.Files != null && messageDTO.Files.Count() >= 1)
                {
                    _documentService.AttachMultipleFiles(message, messageDTO.Files.ToList());
                }

                _messageRepository.Update(message);
                _messageRepository.Save();

                //foreach (var messageReceiver in message.MessageReceivers)
                //{
                //    var receiver = _receiverRepository.GetById(messageReceiver.Receiver.Id, src => src.Include(x => x.User));
                //    _emailService.SendEmail(receiver.User!);
                //}

                return Success(_mapper.Map<SaveMessageDTO>(message));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveMessageDTO>(ex);
            }
        }

        public OperationResult<SaveMessageDTO> ReplyToMessage(ReplyToMessageDTO messageDTO)
        {
            try
            {
                var message = _messageRepository.GetById(messageDTO.Id, src => src.Include(x => x.MessageReceivers)
                                                                                      .ThenInclude(x => x.Receiver)
                                                                                  .Include(x => x.Sender)
                                                                                      .ThenInclude(x => x.User));
                var replyMessage = new Message()
                {
                    Subject = messageDTO.Subject,
                    Body = messageDTO.Body
                };

                foreach (var messageReceiver in message.MessageReceivers)
                {
                    if (messageReceiver.Receiver.UserId != _currentUser.UserId)
                    {
                        replyMessage.MessageReceivers.Add(new MessageReceiver()
                        {
                            Receiver = messageReceiver.Receiver
                        });
                    }
                }

                var receiver = _receiverRepository.Get(x => x.UserId == message.Sender.UserId).FirstOrDefault();
                if (receiver != null)
                {
                    replyMessage.MessageReceivers.Add(new MessageReceiver
                    {
                        ReceiverId = receiver.Id
                    });
                }
                else
                {
                    replyMessage.MessageReceivers.Add(new MessageReceiver
                    {
                        Receiver = new Receiver { UserId = message.Sender.UserId }
                    });
                }

                var sender = _senderRepository.Get(x => x.UserId == _currentUser.UserId.Value).FirstOrDefault();

                if (sender != null)
                {
                    replyMessage.SenderId = sender.Id;
                }
                else
                {
                    replyMessage.Sender = new Sender()
                    {
                        UserId = _currentUser.UserId.Value
                    };
                }

                var emails = new List<string>();

                foreach (var messageReceiver in replyMessage.MessageReceivers)
                {
                    if (messageReceiver.ReceiverId == default(Guid))
                    {
                        var user2 = _userRepository.Get(x => x.Id == messageReceiver.Receiver.UserId).FirstOrDefault();
                        emails.Add(user2.Email!);
                    }
                    else
                    {
                        var user = _receiverRepository.GetById(messageReceiver.ReceiverId, src => src.Include(x => x.User));
                        emails.Add(user.User.Email!);
                    }
                }

                if (messageDTO.Files != null && messageDTO.Files.Count() >= 1)
                {
                    _documentService.AttachMultipleFiles(replyMessage, messageDTO.Files.ToList());
                }

                replyMessage.SyndicElectronicAddress = string.Join(", ", emails);
                message.Reply = replyMessage;
                replyMessage.Parent = message;

                _messageRepository.Update(replyMessage);
                _messageRepository.Save();

                foreach (var messageReceiver in replyMessage.MessageReceivers)
                {
                    var emailReceiver = _receiverRepository.GetById(messageReceiver.ReceiverId, src => src.Include(x => x.User));
                    _emailService.SendEmail(emailReceiver.User!);
                }

                return Success(_mapper.Map<SaveMessageDTO>(_messageRepository.GetById(replyMessage.Id)));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<SaveMessageDTO>(ex);
            }
        }

        public OperationResult<List<DetailsMessageDTO>> GetAllMessageReplies(Guid messageId)
        {
            try
            {
                var message = _messageRepository.GetById(messageId, src => src.Include(x => x.Sender)
                                                                                .ThenInclude(x => x.User)
                                                                              .Include(x => x.MessageReceivers)
                                                                                .ThenInclude(x => x.Receiver)
                                                                                .ThenInclude(x => x.User)
                                                                              .Include(x => x.Reply)
                                                                              .Include(x => x.Parent));
                var replies = new List<Message>() { message };

                if (message != null)
                {
                    GetAllRepliesRecursive(message, replies);
                    GetAllParentsRecursive(message, replies);
                }

                return Success(_mapper.Map<List<DetailsMessageDTO>>(replies.OrderBy(x => x.DateCreated).ToList()));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<List<DetailsMessageDTO>>(ex);
            }
        }

        private void GetAllRepliesRecursive(Message message, List<Message> replies)
        {
            if (message.ReplyId != null)
            {
                var reply = _messageRepository.Get(x => x.Id == message.ReplyId, src => src.Include(x => x.Sender)
                                                                                                   .ThenInclude(x => x.User)
                                                                                                .Include(x => x.MessageReceivers)
                                                                                                    .ThenInclude(x => x.Receiver)
                                                                                                    .ThenInclude(x => x.User)
                                                                                                 .Include(x => x.Reply)
                                                                                                 .Include(x => x.Parent))
                                                                                                    .FirstOrDefault();
                replies.Add(reply!);
                if (reply.Reply != null)
                {
                    GetAllRepliesRecursive(reply.Reply, replies);
                }
            }
        }

        private void GetAllParentsRecursive(Message? message, List<Message> replies)
        {
            if (message.ParentId != null)
            {
                var parent = _messageRepository.Get(x => x.Id == message.ParentId, src => src.Include(x => x.Sender)
                                                                                                       .ThenInclude(x => x.User)
                                                                                                   .Include(x => x.MessageReceivers)
                                                                                                       .ThenInclude(x => x.Receiver)
                                                                                                       .ThenInclude(x => x.User)
                                                                                                   .Include(x => x.Reply)
                                                                                                   .Include(x => x.Parent))
                                                                                                       .FirstOrDefault();
                replies.Add(parent!);
                if (parent.Parent != null)
                {
                    GetAllParentsRecursive(parent.Parent, replies);

                }
            }
        }
    }
}
