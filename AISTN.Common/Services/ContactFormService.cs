using AISTN.Common.Helper;
using AISTN.Common.Models;
using AISTN.Data.DataModel;
using AISTN.Repository;
using AISTN.Repository.Attributes;
using AISTN.Repository.Repository;
using AutoMapper;

namespace AISTN.Common.Services
{
    [Injectable]
    public class ContactFormService : ServiceBase
    {
        private readonly IGenericRepository<ContactForm> _contactFormRepository;

        public ContactFormService(IMapper mapper,
            ExceptionLogger logger,
            IGenericRepository<ContactForm> contactFormRepository,
            ExcelGenerator excelGenerator) : base(mapper, logger, excelGenerator)
        {
            _contactFormRepository = contactFormRepository;
        }

        public OperationResult<ContactFormDTO> GetContactForm()
        {
            try
            {
                var result = _contactFormRepository.GetAll().First();
                return Success(_mapper.Map<ContactFormDTO>(result));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<ContactFormDTO>(ex);
            }
        }

        public OperationResult<ContactFormDTO> CreateContactForm(ContactFormDTO formDTO)
        {
            try
            {
                var forms = _contactFormRepository.GetAll();

                if (forms.Count() >= 1)
                {
                    return Exception<ContactFormDTO>(new Exception("Вече съществува форма за контакт."));
                }

                var form = new ContactForm()
                {
                    RawHtml = formDTO.RawHtml
                };

                _contactFormRepository.Add(form);
                _contactFormRepository.Save();

                return Success(_mapper.Map<ContactFormDTO>(form));
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<ContactFormDTO>(ex);
            }
        }

        public OperationResult<string> UpdateContactForm(ContactFormDTO formDTO)
        {
            try
            {
                var form = _contactFormRepository.Get().First();

                form.RawHtml = formDTO.RawHtml;

                _contactFormRepository.Update(form);
                _contactFormRepository.Save();

                return Success(form.RawHtml!);
            }
            catch (Exception ex)
            {
                _logger.LogException(ex);
                return Exception<string>(ex);
            }
        }
    }
}
