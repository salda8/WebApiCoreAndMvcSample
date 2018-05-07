using System;
using System.Linq;

namespace EF.Web.Models
{
    public class Modal
    {
        public string Id { get; set; }
        public string AreaLabelId { get; set; }
        //public ModalSize Size
        //{
        //    get{

        //    };
        //    set{
        //        switch(this.Size)
        //        {
        //            case ModalSize.Large:
        //                {
        //                    mSize = "modal-lg";
        //                    break;
        //                }
        //                break;
        //        }
        //    };
        //}
    }
    public enum ModalSize
    {
        Small = 0,
        Medium = 1,
        Large = 2
    }
}