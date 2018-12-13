namespace DemoServer.Controllers.Demos.Basics.TheSession
{
    public class TheSessionController
    {
        public void Run()
        {
            #region Demo
            #region Step_1
            using (var session = DocumentStoreHolder.Store.OpenSession())
            #endregion
            {
                #region Step_2
                //   Run your business logic:
                //   
                //   Store documents
                //   Load and Modify documents
                //   Query indexes & collections 
                //   Delete documents
                //   .... etc.
                #endregion
                
                #region Step_3
                session.SaveChanges();
                #endregion
            }
            #endregion
        }
    }
}
