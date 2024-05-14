using AISTN.Common.Models;
using AISTN.Data.DataModel;
using AISTN.PublicAppAPI.Models.Details;
using AISTN.PublicAppAPI.Models.Index;
using AutoMapper;
using Object = AISTN.Data.DataModel.Object;


namespace AISTN.PublicAppAPI.Helper
{
    public class PublicAppAutoMapperProfile : Profile
    {
        public PublicAppAutoMapperProfile()
        {

            #region Nomenclatures

            CreateMap<NomAction, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomActKind, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomActReason, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomCaseKind, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomIncomingDocumentKind, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomInvolvementKind, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSendToKind, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSessionKind, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSessionResult, NomenclatureDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region CustomNomenclatures

            CreateMap<NomSpecialty, NomSpecialityDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomCourt, NomCourtDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomTemplateKind, NomTemplateKindDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomReportType, NomReportTypeDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomReportSource, NomReportSourceDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomMunicipality, NomMunicipalityDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomDistrict, NomDistrictDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSettlement, NomSettlementDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomCaseCode, NomCaseCodeDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSyndicStatusDTO, NomSyndicStatus>().ReverseMap().PreserveReferences();

            #endregion

            #region AnnouncementNomenclatures

            CreateMap<NomAnnouncementStatus, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomLegalBasis, AnnouncementNomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomOfferingKind, AnnouncementNomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomOfferingLocationType, AnnouncementNomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomOfferingProcedure, AnnouncementNomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomObjectKind, AnnouncementNomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomObjectType, NomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomPapersForSale, AnnouncementNomenclatureDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomSalesProcedure, AnnouncementNomenclatureDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region ContactForm

            CreateMap<ContactForm, ContactFormDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Case

            CreateMap<Case, CaseIndexDTO>().ForMember(x => x.SideName, opt => opt.MapFrom(x => x.Sides.FirstOrDefault().Entity.Name))
                                           .ForMember(x => x.Bulstat, opt => opt.MapFrom(x => x.Sides.FirstOrDefault().Entity.Bulstat))
                                           .ForMember(x => x.CaseKindDescription, opt => opt.MapFrom(x => x.CaseKind.Description))
                                           .ForMember(x => x.CourtName, opt => opt.MapFrom(x => x.Court.Name))
                                           .ReverseMap().PreserveReferences();

            CreateMap<Case, AnnouncementCaseIndexDTO>().ForMember(x => x.Side, opt => opt.MapFrom(x => x.Sides.FirstOrDefault(x => x.IsDebtor)))
                                                      .ReverseMap().PreserveReferences();

            CreateMap<Session, CaseBookIndexDTO>().ForMember(x => x.Date, opt => opt.MapFrom(x => x.Date ?? x.Date.Value))
                                                  .ForMember(x => x.Actvity, opt => opt.MapFrom(x => x.SessionKind.Description ?? x.SessionKind.Description))
                                                  .ForMember(x => x.SessionResult, opt => opt.MapFrom(x => x.Acts.FirstOrDefault().Text))
                                                  .ReverseMap().PreserveReferences();

            CreateMap<SurroundDocument, CaseBookIndexDTO>().ForMember(x => x.Date, opt => opt.MapFrom(x => x.Date))
                                                           .ForMember(x => x.Actvity, opt => opt.MapFrom(x => x.Text))
                                                           .ForMember(x => x.ParticipantName, opt => opt.MapFrom(x => x.Sides.FirstOrDefault().Entity != null ?
                                                                                                                      x.Sides.FirstOrDefault().Entity.Name :
                                                                                                                      String.Join(" ", x.Sides.FirstOrDefault().Person.FirstName,
                                                                                                                                       x.Sides.FirstOrDefault().Person.LastName)))
                                                           .ReverseMap().PreserveReferences();


            CreateMap<Case, DetailsCaseDTO>().ForMember(x => x.Sides, opt => opt.MapFrom(x => x.Sides))
                                             .ForMember(x => x.CourtName, opt => opt.MapFrom(x => x.Court.Name))
                                             .ForMember(x => x.CaseKindDescription, opt => opt.MapFrom(x => x.CaseKind.Description))
                                             .ReverseMap().PreserveReferences();


            CreateMap<Session, DetailsSessionDTO>().ForMember(x => x.SessionKind, opt => opt.MapFrom(x => x.SessionKind))
                                                   .ForMember(x => x.Action, opt => opt.MapFrom(x => x.Action))
                                                   .ForMember(x => x.Result, opt => opt.MapFrom(x => x.Result))
                                                   .ReverseMap().PreserveReferences();

            CreateMap<Judge, DetailsJudgeDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Template

            CreateMap<Template, TemplateIndexDTO>().ForMember(x => x.TemplateKindName, opt => opt.MapFrom(x => x.TemplateKind!.Name))
                                                   .ForMember(x => x.DocumentContentID, opt => opt.MapFrom(x => x.DocumentCollection.DocumentContents.FirstOrDefault().Id))
                                                   .ReverseMap().PreserveReferences();

            #endregion

            #region Entity

            CreateMap<Entity, EntityIndexDTO>().ReverseMap().PreserveReferences();
            CreateMap<Entity, DetailsEntityDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Person

            CreateMap<Person, PersonIndexDTO>().ReverseMap().PreserveReferences();
            CreateMap<Person, DetailsPersonDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Trustee

            CreateMap<Trustee, TrusteeIndexDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region ImportedDeed

            CreateMap<ImportedDeed, ImportedDeedIndexDTO>()
                .ForMember(x => x.CourtName, opt => opt.MapFrom(x => x.Case.Court.Name))
                .ForMember(x => x.CourtOfAppealName, opt => opt.MapFrom(x => x.CourtofAppeal.Name))
                .ReverseMap().PreserveReferences();

            #endregion

            #region Side

            CreateMap<Side, SideIndexDTO>().ReverseMap().PreserveReferences();

            CreateMap<Side, DetailsSideDTO>()
                .ForMember(x => x.InvolvementKindName, opt => opt.MapFrom(x => x.InvolvementKind.Description))
                .ForMember(x => x.SideName, opt => opt.MapFrom(x => x.Entity != null ? x.Entity.Name : String.Join(" ", x.Person.FirstName, x.Person.LastName)))

                .ReverseMap().PreserveReferences();


            #endregion

            #region Syndic

            CreateMap<Syndic, SyndicIndexDTO>().ForMember(x => x.FullAddress, opt => opt.MapFrom(x => GetConcatenatedAddress(x.Syndic2Addresses.FirstOrDefault().Address)))
                                               .ForMember(x => x.FullName, opt => opt.MapFrom(x => String.Join(" ", x.FirstName, x.LastName)))
                                               .ForMember(x => x.SyndicSpecialty, opt => opt.MapFrom(x => x.SpecialtyNavigation.Description))
                                               .ReverseMap().PreserveReferences();

            CreateMap<Syndic, DetailsSyndicDTO>().ForMember(x => x.SyndicSpecialty, opt => opt.MapFrom(x => x.SpecialtyNavigation.Description))
                                                 .ForMember(x => x.FullAddress, opt => opt.MapFrom(x => GetConcatenatedAddress(x.Syndic2Addresses.FirstOrDefault().Address)))
                                                 .ForMember(x => x.FullName, opt => opt.MapFrom(x => String.Join(" ", x.FirstName, x.LastName)))
                                                 .ForMember(x => x.OrderDate, opt => opt.MapFrom(x => x.Orders.FirstOrDefault().Date))
                                                 .ForMember(x => x.OrderNumber, opt => opt.MapFrom(x => x.Orders.FirstOrDefault().Number))
                                                 .ForMember(x => x.StateGazetteEdition, opt => opt.MapFrom(x => x.Orders.FirstOrDefault().StateGazetteEdition))
                                                 .ForMember(x => x.StateGazetteYear, opt => opt.MapFrom(x => x.Orders.FirstOrDefault().StateGazetteYear))
                                                 .ReverseMap().PreserveReferences();

            #endregion

            #region StatisticalReport

            CreateMap<StatisticalReport, StatisticalReportIndexDTO>()
                .ForMember(x => x.Type, src => src.MapFrom(x => x.TypeNavigation!.Name))
                .ForMember(y => y.ReportSource, src => src.MapFrom(y => y.ReportSource!.Name))
                .ForMember(y => y.PublishedDate, src => src.MapFrom(y => y.Published))
                .ReverseMap().PreserveReferences();

            CreateMap<StatisticalReport, DetailsStatisticalReportDTO>()
                .ForMember(x => x.Type, opt => opt.MapFrom(x => x.TypeNavigation.Name))
                .ForMember(x => x.Source, opt => opt.MapFrom(x => x.ReportSource.Name))
                .ReverseMap().PreserveReferences();

            #endregion

            #region Document


            CreateMap<DocumentContent, DocumentContentDTO>().ForMember(x => x.BlobDateCreated, opt => opt.MapFrom(x => x.Blob.DateCreated)).ReverseMap().PreserveReferences();
            CreateMap<DocumentCollection, DocumentCollectionDTO>().ReverseMap().PreserveReferences();
            CreateMap<NomAttachedDocumentKind, NomAttachedDocumentKindDTO>().ReverseMap().PreserveReferences();

            #endregion

            #region Address

            CreateMap<Address, AddressIndexDTO>().ReverseMap().PreserveReferences();

            CreateMap<Address, DetailsAddressIndexDTO>().ForMember(x => x.SettlementName, opt => opt.MapFrom(x => x.Settlement.Name))
                                                        .ForMember(x => x.RegionName, opt => opt.MapFrom(x => x.Region.Name))
                                                        .ForMember(x => x.MunicipalityName, opt => opt.MapFrom(x => x.Municipality.Name))
                                                        .ReverseMap().PreserveReferences();
            #endregion

            #region Announcement

            CreateMap<Announcement, AnnouncementIndexDTO>().ForMember(x => x.FullAddress, opt => opt.MapFrom(x => GetConcatenatedAddress(x.Address)))
                                                          .ForMember(x => x.SyndicName, opt => opt.MapFrom(x => String.Join(" ", x.Syndic.FirstName, x.Syndic.LastName)))
                                                          .ForMember(x => x.CourtName, opt => opt.MapFrom(x => x.Court.Name))
                                                          .ForMember(x => x.Status, opt => opt.MapFrom(x => x.StatusCodeNavigation.Description))
                                                          .ReverseMap().PreserveReferences();

            CreateMap<Object, ObjectIndexDTO>().ForMember(x => x.ObjectType, opt => opt.MapFrom(x => x.ObjectType.Description))
                                                          .ForMember(x => x.ObjectKind, opt => opt.MapFrom(x => x.ObjectKind.Description))
                                                          .ForMember(x => x.FullAddress, opt => opt.MapFrom(x => GetConcatenatedAddress(x.Address)))
                                                          .ReverseMap().PreserveReferences();

            CreateMap<Announcement, DetailsAnnouncementDTO>().ForMember(x => x.LegalBasis, opt => opt.MapFrom(x => x.LegalBasis.Description))
                                                             .ForMember(x => x.LocationType, opt => opt.MapFrom(x => x.LocationType.Description))
                                                             .ForMember(x => x.OfferingProcedure, opt => opt.MapFrom(x => x.OfferingProcedure.Description))
                                                             .ForMember(x => x.OfferingKind, opt => opt.MapFrom(x => x.OfferingKind.Description))
                                                             .ForMember(x => x.PapersForSale, opt => opt.MapFrom(x => x.PapersForSale.Description))
                                                             .ForMember(x => x.SalesProcedure, opt => opt.MapFrom(x => x.SalesProcedure.Description))
                                                             .ForMember(x => x.Status, opt => opt.MapFrom(x => x.StatusCodeNavigation.Description))
                                                             .ForMember(x => x.CourtName, opt => opt.MapFrom(x => x.Court.Name))
                                                             .ReverseMap().PreserveReferences();

            CreateMap<Object, DetailsObjectDTO>().ForMember(x => x.ObjectKind, opt => opt.MapFrom(x => x.ObjectKind.Description))
                                                             .ForMember(x => x.ObjectType, opt => opt.MapFrom(x => x.ObjectType.Description))
                                                             .ReverseMap().PreserveReferences();


            #endregion

            #region Administration

            CreateMap<TemplateArticle28, IndexTemplateArticles28DTO>()
               .ForMember(x => x.DirectiveTemplateKindName, opt => opt.MapFrom(x => x.DirectiveTemplateKind.Description))
               .ForMember(x => x.DocumentContentId, opt => opt.MapFrom(x => x.DocumentCollection.DocumentContents.FirstOrDefault().Id)).ReverseMap().PreserveReferences();
            #endregion

            #region DocumentLegalBasis

            CreateMap<DocumentLegalBasis, DocumentLegalBasisIndexDTO>()
                .ForMember(x => x.DocumentContentId, opt => opt.MapFrom(x => x.DocumentCollection.DocumentContents.FirstOrDefault().Id))
                .ReverseMap().PreserveReferences();

            #endregion
        }

        private static string GetConcatenatedAddress(Address source)
        {
            if (source == null) return null;

            List<string> addressParts = new List<string>();
            if (source.Region != null && !string.IsNullOrWhiteSpace(source.Region.Name))
                addressParts.Add(source.Region.Name);
            if (source.Settlement != null && !string.IsNullOrWhiteSpace(source.Settlement.Name))
                addressParts.Add(source.Settlement.Name);
            if (source.Municipality != null && !string.IsNullOrWhiteSpace(source.Municipality.Name))
                addressParts.Add(source.Municipality.Name);
            if (!string.IsNullOrWhiteSpace(source.PostCode))
                addressParts.Add(source.PostCode);
            if (!string.IsNullOrWhiteSpace(source.StreetName))
                addressParts.Add(source.StreetName);
            if (!string.IsNullOrWhiteSpace(source.StreetNumber))
                addressParts.Add(source.StreetNumber);
            if (!string.IsNullOrWhiteSpace(source.BuildingNumber))
                addressParts.Add(source.BuildingNumber);
            if (!string.IsNullOrWhiteSpace(source.EntranceNumber))
                addressParts.Add(source.EntranceNumber);
            if (!string.IsNullOrWhiteSpace(source.FloorNumber))
                addressParts.Add(source.FloorNumber);
            if (!string.IsNullOrWhiteSpace(source.ApartmentNumber))
                addressParts.Add(source.ApartmentNumber);


            return string.Join(", ", addressParts);
        }
    }
}
