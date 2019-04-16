using System.ComponentModel;
using DevExpress.ExpressApp.Model;

namespace Xpand.XAF.Modules.SupressConfirmation{
    public interface IModelClassSupressConfirmation : IModelNode {
        [Category(SupressConfirmationModule.CategoryName)]
        bool SupressConfirmation { get; set; }
    }
    [ModelInterfaceImplementor(typeof(IModelClassSupressConfirmation), "ModelClass")]
    public interface IModelObjectViewSupressConfirmation : IModelClassSupressConfirmation {
    }
}