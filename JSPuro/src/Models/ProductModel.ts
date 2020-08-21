class ProductModel {
    
    constructor(
        private _id: number, 
        private _name: string, 
        private _description: string,
        private _img: string
        ) {}

    get id() {
        return this._id
    }

    get nome() {
        return this._name
    }

    get descricao(){
        return this._description
    }
    
    get img() {
        return this._img
    }

}
