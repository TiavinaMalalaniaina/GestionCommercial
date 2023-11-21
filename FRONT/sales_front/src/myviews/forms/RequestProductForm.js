import React from 'react'
import {
  CButton,
  CForm,
  CFormInput,
  CFormLabel,
  CFormSelect,
  CFormTextarea
} from '@coreui/react'

const RequestProductForm = ({formData, addProduct, handleInputChange, products}) => {

  const toDict=(data)=> {
    let model = []
    data.map(value=>{
      model[value.id]=value.name
    })
    return model;
  }



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
        value={formData.product}
        >
          <option>Choisissez votre produit</option>
          {products.map((product, index)=>
            <option value={product.productId} key={index}>{product.productName}</option>
          )}
        </CFormSelect>
      </div>
      <div className="mb-3">
        <CFormInput type="number" name="quantity" id="request-form-quantity" floatingLabel="Quantité" placeholder="" onChange={handleInputChange}  value={formData.quantity}/>
      </div>
      <div className="mb-3">
        <CFormTextarea type="text" name="reason" id="request-form-reason" floatingLabel="Motif" placeholder="..." onChange={handleInputChange} value={formData.reason}/>
      </div>
      <CButton color="primary" type="submit">Ajouter</CButton>
    </CForm>
  )
}

export default RequestProductForm
