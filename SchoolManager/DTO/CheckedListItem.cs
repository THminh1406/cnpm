namespace SchoolManager.DTO
{
    public class CheckedListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString() => Name;
    }
}
