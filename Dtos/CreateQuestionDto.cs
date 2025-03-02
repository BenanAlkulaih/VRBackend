namespace VRBackend.Dtos
{
    public class CreateQuestionDto
    {
        public string Text { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public List<CreateOptionDto> Options { get; set; } = null!;
    }
}
