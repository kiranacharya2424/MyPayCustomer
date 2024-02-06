using MyPay.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.Models.Get
{
    public class GetVendor_API_CIT_Journey_Details : CommonGet
    {
        private string _responseMessage = string.Empty;
        public string responseMessage
        {
            get { return _responseMessage; }
            set { _responseMessage = value; }
        }

        private string _Message = string.Empty;
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        private string responseCode;
        public string ResponseCode
        {
            get => responseCode;
            set => responseCode = value;
        }
        private string totalProcessSeq;
        public string TotalProcessSeq
        {
            get => totalProcessSeq;
            set => totalProcessSeq = value;
        }
        private List<Datum> data;
        public List<Datum> Data
        {
            get => data;
            set => data = value;
        }
        private string appGroup;
        public string AppGroup
        {
            get => appGroup;
            set => appGroup = value;
        }
        private string appId;
        public string AppId
        {
            get => appId;
            set => appId = value;
        }
    }

    public class DataType
    {
        private string Type;
        public string type
        {
            get => Type;
            set => Type = value;
        }
        private int Length;
        public int length
        {
            get => Length;
            set => Length = value;
        }
        private int MinLength;
        public int minLength
        {
            get => MinLength;
            set => MinLength = value;
        }
    }

    public class Datum
    {
        private int processSeq;
        public int ProcessSeq
        {
            get => processSeq;
            set => processSeq = value;
        }
        private List<RequiredField> requiredFields;
        public List<RequiredField> RequiredFields
        {
            get => requiredFields;
            set => requiredFields = value;
        }
        private List<ResponseFieldMapping> responseFieldMapping;
        public List<ResponseFieldMapping> ResponseFieldMapping
        {
            get => responseFieldMapping;
            set => responseFieldMapping = value;
        }
    }

    public class RequiredField
    {

        private string fieldName;
        public string FieldName
        {
            get => fieldName;
            set => fieldName = value;
        }
        private string fieldLabel;
        public string FieldLabel
        {
            get => fieldLabel;
            set => fieldLabel = value;
        }
        private string fieldType;
        public string FieldType
        {
            get => fieldType;
            set => fieldType = value;
        }
        private DataType dataType;
        public DataType DataType
        {
            get => dataType;
            set => dataType = value;
        }
        private string isRequired;
        public string IsRequired
        {
            get => isRequired;
            set => isRequired = value;
        }
        private string inputFormat;
        public string InputFormat
        {
            get => inputFormat;
            set => inputFormat = value;
        }
        private string addnUrl;
        public string AddnUrl
        {
            get => addnUrl;
            set => addnUrl = value;
        }
    }

    public class ResponseFieldMapping
    {
        private string fieldName;
        public string FieldName
        {
            get => fieldName;
            set => fieldName = value;
        }
        private string mapField;
        public string MapField
        {
            get => mapField;
            set => mapField = value;
        }
        private string fieldLabel;

        public string FieldLabel
        {
            get => fieldLabel;
            set => fieldLabel = value;
        }
    }




}