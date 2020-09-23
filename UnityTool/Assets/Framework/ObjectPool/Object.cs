namespace Codingriver
{
    public abstract class Object : IReference
    {
        public long id { get ;  set ; }

        /// <summary>
        /// 获取对象时的事件。
        /// </summary>
        protected virtual void OnSpawn(object userData)
        {

        }

        /// <summary>
        /// 回收对象时的事件。
        /// </summary>
        protected virtual void OnUnspawn()
        {


        }

        /// <summary>
        /// 对象池释放对象。
        /// </summary>
        protected void OnRelease()
        {

        }

    }
}
