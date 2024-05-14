namespace IRIWCFService.IRIService
{
  
    public class QualifiedTextHelper
    {
        List<QualifiedText> _buffer = new List<QualifiedText>();
        void Add(string lang, string Value)
        {
            _buffer.Add(new QualifiedText() { Value = Value, lang = lang });
        }


    }
}
