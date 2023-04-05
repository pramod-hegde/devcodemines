namespace Services.Core.Validation.PayloadValidation
{
    abstract class ActionBase : IValidator
    {
        protected ValidationNode __currentNode;
        protected DataValidationResult _response = default;

        public ActionBase(ValidationNode currentNode)
        {
            __currentNode = currentNode;
        }

        public abstract IDataValidationResult Validate<T>(T payload, ValidationNode node, ValidationNode currentNodePath, params object[] references);          
    }
}
