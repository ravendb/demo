<?php

namespace RavenDB\Demo\attachments\indexAttachmentDetails;

//region Usings
use RavenDB\Documents\Indexes\AbstractIndexCreationTask;
//endregion

use RavenDB\Demo\common\DocumentStoreHolder;
use RavenDB\Demo\common\models\Employee;

//region Demo
//region Step_1
class Employees_ByAttachmentDetails extends AbstractIndexCreationTask
//endregion
{
    public function __construct()
    {
        //region Step_2
        parent::__construct();

        $this->map = "docs.Employees.Select(employee => new {" .
            "    employee = employee," .
            "    attachments = this.AttachmentsFor(employee)" .
        //endregion
        //region Step_3
            "}).Select(this0 => new {" .
            "    AttachmentNames = Enumerable.ToArray(this0.attachments.Select(x => x.Name))," .
            "    AttachmentContentTypes = Enumerable.ToArray(this0.attachments.Select(x0 => x0.ContentType))," .
            "    AttachmentHashes = Enumerable.ToArray(this0.attachments.Select(x1 => x1.Hash))," .
            "    AttachmentSizes = Enumerable.ToArray(this0.attachments.Select(x2 => x2.Size))" .
            "})";
        //endregion
    }
}
//endregion

class IndexAttachmentDetails
{
    public function __invoke(RunParams $runParams): array
    {
        $attachmentContentType = $runParams->getAttachmentContentType() ?? "image/jpeg";
        $attachmentMinSize = $runParams->getAttachmentMinSize() ?? 18000;

        //region Demo
        $employeesWithMatchingAttachments = [];

        $session = DocumentStoreHolder::getStore()->openSession();
        try {
            //region Step_4
            $employeesWithMatchingAttachments = $session->query(Employee::class, Employees_ByAttachmentDetails::class)
                ->whereEquals("AttachmentContentTypes", $attachmentContentType)
                ->whereGreaterThan("AttachmentSizes", $attachmentMinSize)
                ->toList();
            //endregion
        } finally {
            $session->close();
        }
        //endregion

        return $employeesWithMatchingAttachments;
    }
}

//region Demo
//region Step_5
class IndexEntry
{
    private ?array $attachmentNames = null;
    private ?array $attachmentContentTypes = null;
    private ?array $attachmentHashes = null;
    private ?array $attachmentSizes = null;

    public function getAttachmentNames(): ?array
    {
        return $this->attachmentNames;
    }

    public function setAttachmentNames(?array $attachmentNames): void
    {
        $this->attachmentNames = $attachmentNames;
    }

    public function getAttachmentContentTypes(): ?array
    {
        return $this->attachmentContentTypes;
    }

    public function setAttachmentContentTypes(?array $attachmentContentTypes): void
    {
        $this->attachmentContentTypes = $attachmentContentTypes;
    }

    public function getAttachmentHashes(): ?array
    {
        return $this->attachmentHashes;
    }

    public function setAttachmentHashes(?array $attachmentHashes): void
    {
        $this->attachmentHashes = $attachmentHashes;
    }

    public function getAttachmentSizes(): ?array
    {
        return $this->attachmentSizes;
    }

    public function setAttachmentSizes(?array $attachmentSizes): void
    {
        $this->attachmentSizes = $attachmentSizes;
    }
}
//endregion
//endregion
