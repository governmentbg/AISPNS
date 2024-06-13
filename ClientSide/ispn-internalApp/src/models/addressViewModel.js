class AddressViewModel {
  constructor() {
    this.id = null;
    this.regionId = null; //Област
    this.municipalityId = null; //Община
    this.settlementId = null; //Населено място
    this.postCode = null;
    this.streetNumber = null;
    this.streetName = null;
    this.buildingNumber = null;
    this.entranceNumber = null;
    this.floorNumber = null;
    this.apartmentNumber = null;
    this.additional = null; //Допълнителен адрес
    this.ekkate = null;

    this.districtName = null;
    this.municipalityName = null;
    this.settlementName = null;
  }
}

export default AddressViewModel;
