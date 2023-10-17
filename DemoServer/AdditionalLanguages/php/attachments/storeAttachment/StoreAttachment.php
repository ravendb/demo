<?php

namespace RavenDB\Demo\attachments\storeAttachment;

//region Usings
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;

class StoreAttachment
{
    private const DEFAULT_DOCUMENT_ID = "companies/2-A";

    public function __invoke(RunParams $runParams): string
    {
        $documentId = $runParams->getDocumentId() ?? self::DEFAULT_DOCUMENT_ID;

        $attachmentName = $runParams->getAttachmentName();
        $contentType = $runParams->getContentType();
        $attachment = $runParams->getAttachment();

        //region Demo
        $session = DocumentStoreHolder::getStore()->openSession();
        try {
        
            //region Step_1
            $session->advanced()->attachments()->store($documentId, $attachmentName, $attachment, $contentType);
            //endregion

            //region Step_2
            $session->saveChanges();
            //endregion

        } finally {
            $session->close();
        }
        //endregion

        return "Attachment $attachmentName was stored successfully on document $documentId";
    }
}
