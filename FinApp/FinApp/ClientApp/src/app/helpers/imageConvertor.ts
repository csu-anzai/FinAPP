export default class ImageConvertor {

    static fromFileToFormData(file: File): FormData {
        const formData = new FormData();
        formData.append(file.name, file);
        return formData;
    }
}
