import React from 'react'
import {
  CButton,
  CForm,
  CFormInput,
  CFormLabel,
  CFormSelect,
  CFormTextarea
} from '@coreui/react'

const AddProformaForm = ({formData, addProduct, handleInputChange, products}) => {

  const handleSubmit =(e)=> {
    e.preventDefault()
    const formData = new FormData(e.target)
    addProduct(formData)
  }

  return (
    <CForm onSubmit={handleSubmit}>
      <div className="mb-3">
        <CFormSelect
        aria-label="Default select example"
        name='productId'
        onChange={handleInputChange}
        value={formData.productId}
        >
          <option>Choisissez votre produit</option>
          {products.map((product, index)=>
            <option value={product.product.productId} key={index}>{product.product.productName}</option>
          )}
        </CFormSelect>
      </div>
      <div className="mb-3">
        <CFormInput type="number" name="quantity" id="request-form-quantity" floatingLabel="Quantité" placeholder="" value={formData.quantity} onChange={handleInputChange} />
      </div>
      <div className="mb-3">
        <CFormInput type="number" name="price" id="request-form-quantity" floatingLabel="Prix unitaire" placeholder="" value={formData.price} onChange={handleInputChange}/>
      </div>
      <div className="mb-3">
        <CFormInput type="text" name="HT" id="request-form-quantity" floatingLabel="Prix Hors Taxe" placeholder="" value={formData.price*formData.quantity} disabled/>
      </div>
      <CButton color="primary" type="submit">Ajouter</CButton>
    </CForm>
  )
}

export default AddProformaForm
