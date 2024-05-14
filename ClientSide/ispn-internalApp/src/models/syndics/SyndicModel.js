import AddressViewModel from "../addressViewModel";

export default class SyndicModel {
  constructor() {
    this.firstName = null;
    this.secondName = null;
    this.lastName = null;
    this.secondLastName = null;
    this.address = Object.assign({}, new AddressViewModel());
    this.egn = null;
    this.email = null;
    this.phone = null;
    this.specialty = null;
    this.syndicStatusId = null;
    this.active = false;
    this.locked = false;
    this.notes = null;
    this.order = {}
    this.id = null;
  }
}
