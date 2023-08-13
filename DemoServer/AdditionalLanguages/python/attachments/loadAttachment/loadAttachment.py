#region Usings
from copy import deepcopy
#endregion
from demo_example import RunParamsBase, Example

class RunParams(RunParamsBase):
    def __init__(self, document_id: str, attachment_name: str):
        self.document_id = document_id
        self.attachment_name = attachment_name

class LoadAttachment(Example):
    _DEFAULT_DOCUMENT_ID = "categories/1-A"

    def run(self, run_params: RunParams) -> None:
        documentID = run_params.document_id or self._DEFAULT_DOCUMENT_ID
        attachmentName = run_params.attachment_name

        #region Demo
        with self.document_store_holder.store().open_session() as session:
            #region Step_1
            attachment_exists = session.advanced.attachments.exists(documentID, attachmentName)
            #endregion

            if attachment_exists:
                #region Step_2
                with session.advanced.attachments.get(documentID, attachmentName) as attachment_result:
                #endregion
                    #region Step_3
                    content_type = attachment_result.details.content_type
                    hash = attachment_result.details.hash
                    size = attachment_result.details.size
                    #endregion

                    #region Step_4
                    attachment_data = deepcopy(attachment_result.data)
                    #endregion
        #endregion
