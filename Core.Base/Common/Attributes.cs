using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Base
{
    [AttributeUsage(AttributeTargets.All)]
    public class FieldInfoAttribute : Attribute
    {
        // Private fields.
        private string title;
        private int order;
        private bool visible;
        private string description;

        // This constructor defines two required parameters: name and level.

        public FieldInfoAttribute(string title, int order, bool visible = true, string description = "")
        {
            this.title = title;
            this.order = order;
            this.visible = visible;
            this.description = description;
        }

        // Define Name property.
        // This is a read-only attribute.

        public virtual string Title
        {
            get { return title; }
        }


        public virtual int Order
        {
            get { return order; }
        }


        public virtual bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public virtual string Description
        {
            get { return description; }
        }

    }



    //[AttributeUsage(AttributeTargets.Class)]
    //public class ModelDbAttribute : Attribute
    //{
    //    private Constant.CommandType commandType;
    //    private string domainName;
    //    //private dynamic parameters;

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="CommandType"></param>
    //    /// <param name="Name"></param> Table name or SP name or view Name
    //    public ModelDbAttribute(Constant.CommandType CommandType, string DomainName/*, dynamic Parameters = null*/)
    //    {
    //        this.commandType = CommandType;
    //        this.domainName = DomainName;
    //        //if (Parameters != null)
    //        //{
    //        //    parameters = Parameters;
    //        //}
    //    }


    //public virtual Constant.CommandType CommandType
    //{
    //    get { return commandType; }
    //}
    ////public virtual dynamic Parameters
    ////{
    ////    get { return parameters; }
    ////}

    //public virtual string DomainName
    //{
    //    get { return domainName; }
    //}

    
}
