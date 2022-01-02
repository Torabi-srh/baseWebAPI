namespace baseWebAPI
{
    public class MethodResult<T>
    {
        public MethodResult() { }
        public MethodResult(T result) {
            this.Result = result;
        }
        public CRUDResultModel ResultModel { get; set; }
        //public IEnumerable<MethodError> Errors { get; set; }
        public T Result { get; set; }
    }
}
