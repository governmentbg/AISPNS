import AddressViewModel from "../addressViewModel";

export default class SyndicModel {
  constructor() {
    this.number = null;
    this.firstName = null;
    this.middleName = null;
    this.lastName = null;
    this.secondLastName = null;
    this.address = Object.assign({}, new AddressViewModel());
    this.egn = null;
    this.email = null;
    this.phone = null;
    this.specialty = null;
    this.status = null;
    this.isActive = true;
    this.notes = null;
    this.lastModified = null;
    this.dateCreated = null;
    this.order = {
      number: null,
      date: null,
      dvNumber: null,
      dvYear: null
    }
    this.id = null;
  }
}
