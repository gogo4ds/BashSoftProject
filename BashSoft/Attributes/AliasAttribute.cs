namespace BashSoft.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class AliasAttribute : Attribute
    {
        public AliasAttribute(string aliasName)
        {
            this.Name = aliasName;
        }

        public string Name { get; }

        public override bool Equals(object obj)
        {
            return this.Name.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}