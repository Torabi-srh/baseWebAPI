namespace baseWebAPI
{
    public class MethodError
    {
        public MethodError()
        {

        }
        public MethodError(int code, string title, string description)
        {
            Code = code;
            Title = title;
            Description = description;
        }
        public MethodError(int code, string title, string description,string filedName)
        {
            Code = code;
            Title = title;
            Description = description;
            FiledName = filedName;
        }

        public int Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FiledName { get; set; }

        public override string ToString()
        {
            return $"{Code.ToString()} {Title} {Description}";
        }
    }
}
