using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace baseWebAPI
{
    public class CRUDResultModel
    {
        public CRUDResultEnum Type { get; set; }
        public string TypeString => Type.ToString();
        public int TypeNumber => (int)Type;
        public string Message { get; set; }
        public int? MessageCode { get; set; }
        public IEnumerable<MethodError> Errors { get; set; } = null;
        IStringLocalizer<SharedResource> SharedResource { get; set; } = null;

        public CRUDResultModel()
        {

        }
        public CRUDResultModel(CRUDResultEnum type)
        {
            Type = type;
            SharedResource = null;
            Message = GetDefaultMessage(type, SharedResource);
        }
        //[Obsolete("از این متد بدلیل عدم استفاده از منابع مشترک استفاده نکنید", false)]
        //public CRUDResultModel(CRUDResultEnum type)
        //{
        //    Type = type;
        //    Message = GetDefaultMessage(type, SharedResource);
        //}
        public CRUDResultModel(CRUDResultEnum type, IStringLocalizer<SharedResource> sharedResource )
        {
            Type = type;
            SharedResource = sharedResource;
            Message = GetDefaultMessage(type, SharedResource);
        }
        public static string GetDefaultMessage(CRUDResultEnum type, IStringLocalizer<SharedResource> sharedResource)
        {
            switch (type)
            {
                case CRUDResultEnum.Success:
                    return sharedResource == null ? "عملیات با موفقیت انجام شد" : sharedResource["عملیات با موفقیت انجام شد"];
                case CRUDResultEnum.UnknownError:
                    return sharedResource == null ? "خطای ناشناخته ای رخداده است، لطفا مجددا تلاش کنید" : sharedResource["خطای ناشناخته ای رخداده است، لطفا مجددا تلاش کنید"];
                //case CRUDResultEnum.CodeExist:
                //    return sharedResource == null ? "کد وارد شده تکراری می باشد" : sharedResource["کد وارد شده تکراری می باشد"];
                //case CRUDResultEnum.RecordNotExist:
                //    return sharedResource == null ? "رکورد درخواستی وجود ندارد" : sharedResource["رکورد درخواستی وجود ندارد"];
                //case CRUDResultEnum.DateIsDuplicate:
                //    return sharedResource == null ? "تاریخ تکراری می باشد" : sharedResource["تاریخ تکراری می باشد"];
                //case CRUDResultEnum.ChangeStatusDenied:
                    //return sharedResource == null ? "مجوز تغییر وضعیت وجود ندارد" : sharedResource["مجوز تغییر وضعیت وجود ندارد"];
                //case CRUDResultEnum.ModelIsInvalid:
                //    return sharedResource == null ? "مدل ارسالی معتبر نمی باشد" : sharedResource["مدل ارسالی معتبر نمی باشد"];
                case CRUDResultEnum.Error:
                    return sharedResource == null ? "خطایی رخداده است" : sharedResource.GetString("خطایی رخداده است");
                case CRUDResultEnum.AccessDenied:
                    return sharedResource == null ? "شما مجوز دسترسی به این عملیات را ندارید" : sharedResource["شما مجوز دسترسی به این عملیات را ندارید"];
                default:
                    return string.Empty;
            }
        }
    }
}
