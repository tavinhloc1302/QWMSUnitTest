using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QWMSServer.Model.ViewModels;
using QWMSServer.Data.Common;

namespace QWMSServer.Data.Services
{
    public static class ResponseConstructor<T> where T : class
    {
        public static ResponseViewModel<T> ConstructEnumerableData(int errorCode, IEnumerable<T> datas)
        {
            ResponseViewModel<T> ret = new ResponseViewModel<T>();
            ret.errorCode = errorCode;
            ret.errorText = GetErrorText(errorCode);
            ret.responseData = null;
            ret.responseDatas = datas;
            return ret;
        }

        public static ResponseViewModel<T> ConstructEnumerableData(int errorCode, string errorText, IEnumerable<T> datas)
        {
            ResponseViewModel<T> ret = new ResponseViewModel<T>();
            ret.errorCode = errorCode;
            ret.errorText = errorText;
            ret.responseData = null;
            ret.responseDatas = datas;
            return ret;
        }

        public static ResponseViewModel<T> ConstructData(int errorCode, T data)
        {
            ResponseViewModel<T> ret = new ResponseViewModel<T>();
            ret.errorCode = errorCode;
            ret.errorText = GetErrorText(errorCode);
            ret.responseData = data;
            ret.responseDatas = null;
            return ret;
        }

        public static ResponseViewModel<T> ConstructData(int errorCode, string errorText, T data)
        {
            ResponseViewModel<T> ret = new ResponseViewModel<T>();
            ret.errorCode = errorCode;
            ret.errorText = errorText;
            ret.responseData = data;
            ret.responseDatas = null;
            return ret;
        }

        public static ResponseViewModel<T> ConstructBoolRes(int errorCode, bool res)
        {
            ResponseViewModel<T> ret = new ResponseViewModel<T>();
            ret.errorCode = errorCode;
            ret.errorText = GetErrorText(errorCode);
            ret.responseData = null;
            ret.responseDatas = null;
            ret.booleanResponse = res;
            return ret;
        }

        private static String GetErrorText(int errorCode)
        {
            string errorText;
            switch (errorCode)
            {
                case ResponseCode.ERR_DB_CONNECTION_FAILED:
                    errorText  = ResponseText.ERR_DB_CONNECTION_FAILED_VI;
                    break;
                case ResponseCode.ERR_SEC_NOT_FOUND_GATEPASS:
                    errorText = ResponseText.ERR_SEC_NOT_FOUND_GATEPASS_VI;
                    break;
                case ResponseCode.ERR_SEC_NOT_FOUND_RFID:
                    errorText = ResponseText.ERR_SEC_NOT_FOUND_RFID_VI;
                    break;
                case ResponseCode.ERR_SEC_NOT_PERMITTED_CHECK_IN:
                    errorText = ResponseText.ERR_SEC_NOT_PERMITTED_CHECKIN_VI;
                    break;
                case ResponseCode.ERR_SEC_NOT_PERMITTED_CHECK_OUT:
                    errorText = ResponseText.ERR_SEC_NOT_PERMITTED_CHECKOUT_VI;
                    break;
                case ResponseCode.ERR_SEC_NOT_PERMITTED_REG:
                    errorText = ResponseText.ERR_SEC_NOT_PERMITTED_REG_VI;
                    break;
                case ResponseCode.ERR_SEC_NOT_SUPPORT_CONDITION:
                    errorText = ResponseText.ERR_SEC_NOT_SUPPORT_CONDITION;
                    break;
                case ResponseCode.ERR_SEC_NOT_SUPPORT_UPDATED_STATE:
                    errorText = ResponseText.ERR_SEC_NOT_SUPPORT_UPDATED_STATE;
                    break;
                case ResponseCode.ERR_SEC_WRONG_BODY_REQUEST_FORMAT:
                    errorText = ResponseText.ERR_SEC_WRONG_BODY_REQUEST_FORMAT;
                    break;
                case ResponseCode.ERR_SEC_WRONG_CONFIRMED_RFID:
                    errorText = ResponseText.ERR_SEC_WRONG_CONFIRMED_RFID_VI;
                    break;
                case ResponseCode.SUCCESS:
                    errorText = ResponseText.SUCCESS;
                    break;
                case ResponseCode.ERR_SEC_NOT_PERMIT_PASS_SECURITY_GATE:
                    errorText = ResponseText.ERR_SEC_NOT_PERMIT_PASS_SECURITY_GATE_VI;
                    break;
                case ResponseCode.ERR_SEC_UNKNOW:
                    errorText = ResponseText.ERR_SEC_UNKNOW;
                    break;
                case ResponseCode.ERR_QUE_NO_QUEUE_FOUND:
                    errorText = ResponseText.ERR_QUE_NO_QUEUE_FOUND_VI;
                    break;
                case ResponseCode.ERR_QUE_NO_GATEPASS_FOUND:
                    errorText = ResponseText.ERR_QUE_NO_GATEPASS_FOUND_VI;
                    break;
                case ResponseCode.ERR_LOGIN_WRONG_USERNAME_PASS:
                    errorText = ResponseText.ERR_LOGIN_WRONG_USERNAME_PASS;
                    break;
                case ResponseCode.ERR_USER_NOT_EXSIT:
                    errorText = ResponseText.ERR_USER_NOT_EXSIT_VI;
                    break;
                case ResponseCode.ERR_INVALID_LOGIN:
                    errorText = ResponseText.ERR_INVALID_LOGIN_VI;
                    break;
                case ResponseCode.ERR_USER_PERMISSION:
                    errorText = ResponseText.ERR_USER_PERMISSION;
                    break;
                default:
                    errorText = ResponseText.ERR_SEC_UNKNOW;
                    break;
            }
            return errorText;
        }
    }
}
