namespace UserRegistration.API.DTOS.Responses
{
    public class ImageResultDTO
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string Description { get; set; }

        public byte[] Content {  get; set; }

        public int Size { get; set; }

        public string ContentType { get; set; }

    }
}
