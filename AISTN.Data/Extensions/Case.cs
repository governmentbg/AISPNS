namespace AISTN.Data.DataModel
{
    public partial class Case : IEntity
    {
        public Side? GetCaseSide(Guid sideId)
        {
            return 
                //if there is a case side
                this.Sides.SingleOrDefault(x => x.Id == sideId) ??
                //if there is a surround side
                this.SurroundDocuments.SelectMany(x => x.Sides).SingleOrDefault(x => x.Id == sideId) ??
                ////if there is a appeal side
                this.Sessions.SelectMany(x => x.Acts).SelectMany(x => x.Appeals).SelectMany(x => x.Sides).SingleOrDefault(x => x.Id == sideId)
                //if none
                ?? null;
        }
    }
}