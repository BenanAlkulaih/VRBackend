namespace VRBackend.Dtos
{
    public class EditQuestionDto
    {
        public string Text { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public List<EditOptionDto> Options { get; set; } = null!;
    }
}
