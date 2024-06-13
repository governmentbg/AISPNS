import AddressViewModel from "../addressViewModel";

export default class SyndicTrainingModel {
  constructor() {
    this.program = null;
    this.email = null;
    this.phone = null;
    this.mainTraining = {
      course: null,
      hotelReservation: null,
      isLector: null,
      fromDate: null,
      toDate: null
    };
    this.alternateTraining = {
      course: null,
      hotelReservation: null,
      isLector: null,
      fromDate: null,
      toDate: null
    };
    this.id = null;
  }
}
