﻿using System.Collections.Generic;
using System.Linq;
using System;
using System.Linq.Dynamic.Core;

namespace Services.Core.Validation.PayloadValidation
{
    public class Connector : IConnector
    {
        ValidationNode IConnector.Next(IDataValidationResult result, ValidationNode node, ValidationNode currentValidationPath, object[] references)
        {            
            if (result.InternalResult == InternalValidationStepResult.None && result.ValidationResult == ValidationResults.ValidationCompleted && result.PostValidationAction == PostValidationActions.NoAction)
            {
                ValidationNode.ValidationFlow.Add(node);
                return null;
            }
            else if (result.InternalResult == InternalValidationStepResult.None && result.ValidationResult == ValidationResults.ValidationCompleted && result.PostValidationAction == PostValidationActions.SaveData)
            {
                ValidationNode.ValidationFlow.Add(node);
                return null;
            }
            else if (result.InternalResult == InternalValidationStepResult.None && result.ValidationResult == ValidationResults.ValidationCompleted && result.PostValidationAction == PostValidationActions.ProcessFurther)
            {
                ValidationNode.ValidationFlow.Add(node);
                return null;
            }
            else if (result.InternalResult == InternalValidationStepResult.ExitAndFalse && result.ValidationResult == ValidationResults.ValidationCompleted)
            {
                ValidationNode.ValidationFlow.Add(node);
                return null;
            }
            else if (result.InternalResult == InternalValidationStepResult.True)
            {
                ValidationNode.ValidationFlow.Add(node);
                return currentValidationPath.Sequences.First(x => x.Id == node.ParentId).Sequences.First(x => x.Id == node.OnTrue);
            }
            else if (result.InternalResult == InternalValidationStepResult.False)
            {
                ValidationNode.ValidationFlow.Add(node);
                return currentValidationPath.Sequences.First(x => x.Id == node.ParentId).Sequences.First(x => x.Id == node.OnFalse);
            }
            else if ((string.Compare(node.Type, ValidationActions.Transform.ToString()) == 0) && result.ValidationResult == ValidationResults.ValidationCompleted && result.InternalResult == InternalValidationStepResult.Transform)
            {
                ValidationNode.ValidationFlow.Add(node);
                return currentValidationPath.Sequences.First(x => x.Id == node.ParentId).Sequences.First(x => x.Id == node.NextNode);
            }
            else if (result.InternalResult == InternalValidationStepResult.Remove)
            {
                ValidationNode.ValidationFlow.Add(node);
                return null;
            }
            else if (string.Compare(node.Type, ValidationActions.LoopElimination.ToString()) == 0)
            {
                ValidationNode.ValidationFlow.Add(node);
                ValidationNode n = currentValidationPath.Sequences.First(x => x.Id == node.LoopNode);
                dynamic collection = result.Payload.GetType().GetProperty(node.LoopOnProperty).GetValue(result.Payload, null);
                List<IDataValidationResult> responses = new List<IDataValidationResult>();
                List<IDataValidationResult> payloadToAdd = new List<IDataValidationResult>();
                foreach (var child in collection)
                {
                    IDataValidationResult response = n.Validate(child, currentValidationPath, new [] { result.Payload});                    
                    if (!(response.InternalResult == InternalValidationStepResult.Remove))
                    {
                        responses.Add(response);

                        payloadToAdd.Add(response);
                    }
                }

                //dynamic newCollection = result.Payload.GetType().GetProperty(node.LoopOnProperty).GetValue(result.Payload);
                collection.Clear();

                if (payloadToAdd.Any())
                {
                    foreach (var response in payloadToAdd)
                    {
                        collection.Add(response.Payload);
                    }                    
                }

                result.Payload.GetType().GetProperty(node.LoopOnProperty).SetValue(result.Payload, collection);
                //var nextNode = currentValidationPath.Sequences.First(x => x.Id == node.ParentId).Sequences.First(x => x.Id == node.NextNode);
                //var res = nextNode.Validate(logger, result.Payload, currentValidationPath, references);
                //return nextNode.Connector.Next(logger, res, nextNode, currentValidationPath, references);
                return currentValidationPath.Sequences.First(x => x.Id == node.ParentId).Sequences.First(x => x.Id == node.NextNode);

            }
            else if (string.Compare(node.Type, ValidationActions.LoopValidation.ToString()) == 0)
            {
                ValidationNode.ValidationFlow.Add(node);
                ValidationNode n = currentValidationPath.Sequences.First(x => x.Id == node.LoopNode);
                dynamic collection = result.Payload.GetType().GetProperty(node.LoopOnProperty).GetValue(result.Payload, null);
                List<IDataValidationResult> responses = new List<IDataValidationResult>();
                foreach (var child in collection)
                {
                    IDataValidationResult response = n.Validate(child, currentValidationPath, new[] { result.Payload });
                    responses.Add(response);
                }

                bool flag = false;
                foreach (var response in responses)
                {
                    if (response.InternalResult == InternalValidationStepResult.ExitAndFalse)
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag)
                    return currentValidationPath.Sequences.First(x => x.Id == node.ParentId).Sequences.First(x => x.Id == node.OnFalse);
                else
                    return currentValidationPath.Sequences.First(x => x.Id == node.ParentId).Sequences.First(x => x.Id == node.OnTrue);
            }

            //result.ValidationFlow.Add(node);  
            
            throw new ValidationException("Validation Failed, Result unkown", new object[] { node, result, references });
            
        }
    }    
}
