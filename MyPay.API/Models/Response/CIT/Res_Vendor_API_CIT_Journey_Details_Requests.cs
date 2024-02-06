using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPay.API.Models.CIT
{
    public class Res_Vendor_API_CIT_Journey_Details_Requests : CommonResponse
    { 
        
      

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
        private List<CITDatum> data;
        public List<CITDatum> Data
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
    public class CITDataType
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

    public class CITDatum
    {
        private int processSeq;
        public int ProcessSeq
        {
            get => processSeq;
            set => processSeq = value;
        }
        private List<CITRequiredField> requiredFields;
        public List<CITRequiredField> RequiredFields
        {
            get => requiredFields;
            set => requiredFields = value;
        }
        private List<CITResponseFieldMapping> responseFieldMapping;
        public List<CITResponseFieldMapping> ResponseFieldMapping
        {
            get => responseFieldMapping;
            set => responseFieldMapping = value;
        }
    }

    public class CITRequiredField
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
        private CITDataType dataType;
        public CITDataType DataType
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

    public class CITResponseFieldMapping
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