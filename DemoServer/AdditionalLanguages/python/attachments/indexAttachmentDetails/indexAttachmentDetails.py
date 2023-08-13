from typing import List
from demo_example import RunParamsBase, Example
from models import Employee

#region Usings
from ravendb import AbstractIndexCreationTask
#endregion

class RunParams(RunParamsBase):
    def __init__(self, attachment_content_type: str, attachment_min_size: int):
        self.attachment_content_type = attachment_content_type
        self.attachment_min_size = attachment_min_size


class IndexAttachmentDetails(Example):
    #region Demo
    #region Step_1
    class Employees_ByAttachmentDetails(AbstractIndexCreationTask):
    #endregion
        #region Step_2
        class IndexEntry:
            def __init__(
                self,
                attachment_names: List[str],
                attachment_content_types: List[str],
                attachment_hashes: List[str],
                attachment_sizes: List[str],
            ):
                self.attachment_names = attachment_names
                self.attachment_content_types = attachment_content_types
                self.attachment_hashes = attachment_hashes
                self.attachment_sizes = attachment_sizes

        #endregion

        def __init__(self):
            #region Step_3
            super().__init__()
            self.map = (
                "docs.Employees.Select(employee => new{" + "    employee = employee,"
                                                           "    attachments = this.AttachmentsFor(employee)"
            #endregion
            #region Step_4
                                                           "}).Select(this0 => new {"
                                                           "    attachment_names = Enumerable.ToArray(this0.attachments.Select(x => x.Name)),"
                                                           "    attachment_content_types = Enumerable.ToArray(this0.attachments.Select(x0 => x0.ContentType)),"
                                                           "    attachment_hashes = Enumerable.ToArray(this0.attachments.Select(x1 => x1.Hash)),"
                                                           "    attachment_sizes = Enumerable.ToArray(this0.attachments.Select(x2 => x2.Size))"
                                                           "})"
            )
            #endregion
    #endregion

    def run(self, run_params: RunParams) -> List[Employee]:
        attachmentContentType = run_params.attachment_content_type or "image/jpeg"
        attachmentMinSize = run_params.attachment_min_size or 18000

        #region Demo
        with self.document_store_holder.store().open_session() as session:
            #region Step_5
            employees_with_matching_attachments = list(
                session.query_index_type(IndexAttachmentDetails.Employees_ByAttachmentDetails, Employee)
                .where_equals("AttachmentContentTypes", attachmentContentType)
                .where_greater_than("AttachmentSizes", attachmentMinSize)
            )
            #endregion
        #endregion

        return employees_with_matching_attachments
