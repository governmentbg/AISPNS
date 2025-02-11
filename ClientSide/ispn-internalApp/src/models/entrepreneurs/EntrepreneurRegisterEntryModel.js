import AddressViewModel from "../addressViewModel";

export default class EntrepreneurRegisterEntryModel {
  constructor(){
    this.id = null;
    this.actId = null;
    this.proprietorId = null;
    this.fieldId = null;
    this.date = null;
    this.description = null;
    this.legalBasisId = null;
    this.isEnforcedImmediately = null;
    this.appealTerm = null;
    this.announcedByUserId = null;
    this.announcedDate = null;
    this.replacedByEntryId = null
    this.syndicId = null;
	this.registerSyndicKindId = null;
    this.phoneNumber = null;
    this.email = null;
    this.address = new AddressViewModel();
    this.documentCollectionId = null;
  }
}