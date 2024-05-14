export default class MessageModel {
  constructor() {
    this.number = null;
    this.sendDate = null;
    this.receivedDate = null;
    this.sendToAll = false;
    this.messageReceiverIDs = [];
    this.messageAttachments = [];
    this.syndicElectronicAddress = null;
    this.sender = {
      id: null,
      fullName: null,
      roles: null,
      userId: null
    };
    this.subject = null;
    this.body = null;
    this.id = null;
  }
}
