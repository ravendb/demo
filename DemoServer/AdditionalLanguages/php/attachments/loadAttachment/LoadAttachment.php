<?php

namespace RavenDB\Demo\attachments\loadAttachment;

use RavenDB\Demo\common\DocumentStoreHolder;

//region Usings
//endregion

class LoadAttachment
{
    private const DEFAULT_DOCUMENT_ID = "categories/1-A";

    public function __invoke(RunParams $runParams): string
    {
        $documentId = $runParams->getDocumentId() ?? self::DEFAULT_DOCUMENT_ID;

        $attachmentName = $runParams->getAttachmentName();
        $attachmentExists = false;

        //region Demo
        $session = DocumentStoreHolder::getStore()->openSession();
        try {
            //region Step_1
            $attachmentExists = $session->advanced()->attachments()->exists($documentId, $attachmentName);
            //endregion

            if ($attachmentExists) {
                //region Step_2
                $attachmentResult = $session->advanced()->attachments()->get($documentId, $attachmentName);
                //endregion
                try {

                    //region Step_3
                    $contentType = $attachmentResult->getDetails()->getContentType();
                    $hash = $attachmentResult->getDetails()->getHash();
                    $size = $attachmentResult->getDetails()->getSize();
                    //endregion

                    //region Step_4
                    $bytes = $attachmentResult->getData();
                    //endregion

                //region Step_5
                } finally {
                    $attachmentResult->close();
                }
                //endregion

            }
        } finally {
            $session->close();
        }
        //endregion

        if ($attachmentExists)
        {
            return "Attachment $attachmentName was loaded successfully";
        }

        return "Attachment $attachmentName doesn't exist on document $documentId";
    }
}
