import AddressViewModel from "./addressViewModel";

class UserModel {
  constructor() {
    this.id = null;
    this.isActive = true;
    this.dateCreated = null;
    this.lastModified = null;
    this.userName = null
    this.person = {
      firstName: null,
      middleName: null,
      lastName: null,
      egn: null,
      id: null,
      email: null,
    }
    this.roles = [];
  }
}

export default UserModel;
