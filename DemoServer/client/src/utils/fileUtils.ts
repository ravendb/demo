export const fileSizeLimitMB = 5;
const fileSizeLimit = fileSizeLimitMB * 1024 * 1024;

export function isInvalidFileSize(file: File) {
    return file && file.size > fileSizeLimit;
}