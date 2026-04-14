namespace ProyectoFILHA.Models.DTOs
{
    public class TriviaResponse
    {
        public int response_code { get; set; }
        public List<TriviaQuestion> results { get; set; }
    }

}
