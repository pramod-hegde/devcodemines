using System.Collections.Generic;
using System;
using System.Linq;

namespace Services.Core.Validation.PayloadValidation
{
    public class ValidationNode
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Input { get; set; }
        public string ReferenceObject { get; set; }
        public string Expression { get; set; }
        public string OnTrue { get; set; }
        public string EventType { get; set; }
        public string OnFalse { get; set; }
        public string LoopOnProperty { get; set; }
        public string NextNode { get; set; }
        public string LoopNode { get; set; }
        public string Type { get; set; }
        public List<ValidationNode> Sequences { get; set; }
        public static List<ValidationNode> ValidationFlow = new List<ValidationNode>();
        IValidator _validator;
        public IConnector Connector;

        public ValidationNode()
        {            
            Connector = new Connector();
            Sequences = new List<ValidationNode>();            
        }

        private void InitIntegrators()
        {
            try
            {
                ValidationActions type = (ValidationActions)Enum.Parse(typeof(ValidationActions), Type, true);
               _validator= (IValidator)Activator.CreateInstance(GetActionHandle(type), this);
            }
            catch (Exception ex)
            {
                _validator = null;
            }
        }

        private Type GetActionHandle(ValidationActions type)
        {
            return GetType().Assembly.GetTypes().FirstOrDefault(x => x.IsClass &&
                                        x.GetCustomAttributes(typeof(NodeTypeAttribute), true).Length > 0 &&
                                        ((NodeTypeAttribute)x.GetCustomAttributes(typeof(NodeTypeAttribute), true).First()).Action == type);
        }

        internal IDataValidationResult Validate<T>(T payload, ValidationNode currentValidationPath, params object[] references)
        {
            InitIntegrators();
            try
            {
                var response = _validator.Validate(payload, this, currentValidationPath, references);
                if (!(this.Type == ValidationActions.Rule.ToString() || this.Type == ValidationActions.Sequence.ToString()))
                {
                    ValidationNode nextNode = Connector.Next(response, this, currentValidationPath, references);                    
                    return nextNode == null ? response : nextNode.Validate(payload, currentValidationPath, references);
                }
                return response;
            }
            catch (Exception ex)
            {
                var response = new DataValidationResult
                {
                    ValidationResult = ValidationResults.DataValidationFailure,
                    PostValidationAction = PostValidationActions.NoAction,
                    ValidationException = new ValidationException(ex.Message),
                    Payload = payload
                };
                response.ValidationFlow.AddRange(ValidationFlow);
                throw new ValidationException(ex.Message, new object[] {this, response, references });
            }
        }

        //public static implicit operator ValidationNode(string value)
        //{
        //    JObject v = JsonConvert.DeserializeObject<JObject>(value);
        //    return new ValidationNode();
        //}
    }
}
