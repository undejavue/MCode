﻿using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace MCode.Web
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class MService
    {
        [OperationContract]
        public void DoWork()
        {
            // Добавьте здесь реализацию операции
            return;
        }

        // Добавьте здесь дополнительные операции и отметьте их атрибутом [OperationContract]
    }
}
