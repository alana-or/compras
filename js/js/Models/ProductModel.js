class ProductModel {
    constructor(_id, _name, _description, _img) {
        this._id = _id;
        this._name = _name;
        this._description = _description;
        this._img = _img;
    }
    get id() {
        return this._id;
    }
    get nome() {
        return this._name;
    }
    get descricao() {
        return this._description;
    }
    get img() {
        return this._img;
    }
}
