namespace Imperium.Server
{
    public class ResponsePair<T>
    {
        public string[] Groups { get; set; }
        
        public ResponseManager<T>.ResponseGenerator ResponseGenerator { get; set; }



        public ResponsePair(ResponseManager<T>.ResponseGenerator responseGenerator, params string[] groups)
        {
            Groups = groups;
            ResponseGenerator = responseGenerator;
        } 
    }
}