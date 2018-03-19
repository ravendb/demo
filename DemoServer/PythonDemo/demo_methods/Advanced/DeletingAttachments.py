from demo.holder import DocumentStoreHolder


class DeletingAttachments(object):
    @staticmethod
    def deleting_attachments(attachmentName = "the_big_cheese_logo.png"):
		with DocumentStoreHolder.get_store().open_session() as session:
			session.advanced.attachment.delete("companies/77-A", name=attachmentName)
			session.save_changes()

        return "{0} deleted successfully".format(attachmentName);
