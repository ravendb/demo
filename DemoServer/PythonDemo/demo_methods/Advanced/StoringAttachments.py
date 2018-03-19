from demo.holder import DocumentStoreHolder


class StoringAttachments(object):
    @staticmethod
    def storing_attachments(attachmentName = "the_big_cheese_logo.png", attachmentPath = "Images/big_cheese_logo.png"):
        with open(attachmentPath,'rb') as file_stream:
			with DocumentStoreHolder.get_store().open_session() as session:
				session.advanced.attachment.store("companies/77-A", name=attachmentName, stream=file_stream,
												   content_type="image/png")

				session.save_changes()

        return "{0} attached successfully".format(attachmentName);
