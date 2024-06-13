import AddressViewModel from "../addressViewModel";

export default class TrainingModel {
  constructor() {
    this.type = null;
    this.topic = null;
    this.fromDate1 = null;
    this.toDate1 = null;
    this.lengthDate1 = null;
    this.address1Navigation = Object.assign({}, new AddressViewModel());
    this.fromDate2 = null;
    this.toDate2 = null;
    this.lengthDate2 = null;
    this.address2Navigation = Object.assign({}, new AddressViewModel());
    this.maximumParticipants = null;
    this.lecturers = null;
    this.subTopics = null;
    this.examDate1 = null;
    this.examDate2 = null;
    this.programId = null;
    this.id = null;
  }
}
