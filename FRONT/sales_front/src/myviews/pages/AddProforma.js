import CIcon from "@coreui/icons-react"
import { CButton, CCard, CCardBody, CCardTitle, CCol, CFormSelect, CRow } from "@coreui/react"
import { cilPlus } from "@coreui/icons"
import AddProformaModal from "../modals/AddProformaModal"
import { useEffect, useState } from "react"
import AddProformaProductModal from "../modals/AddProformalProductModal"
import AddProformaForm from "../forms/AddProformaForm"
import AddProformaTable from "../tables/AddProformaTable"
import API_CONFIG from "src/apiconfig"
import { toDictSupplier } from "src/utils/dict"

const retour = {

}



const AddProforma=()=>{
    const [data, setData] = useState([])
    const [suppliers, setSuppliers] = useState([])
    const [action, setAction] = useState('Ajouter')
    const [products, setProducts] = useState([])
    const [formData, setFormData] = useState({
        supplierId: '',
        productId: '',
        quantity: '',
        price: '',
        key: ''
    })
    const updateProduct=(index)=> {
      setFormData((prevFormData) => ({
        ...prevFormData,
        productId: data[index].productId,
        quantity: data[index].quantity,
        price: data[index].price,
        key: index
      }));
      setAction('Mettre à jour')
    }

    const addProduct=()=> {
        if (formData.key === '') {
            setData([...data, {productId:formData.productId, quantity:formData.quantity, price:formData.price}])
            setFormData((prevFormData) => ({
                ...prevFormData,
                key: '',
                productId: '',
                quantity: '',
                price: ''
            }))
        } else {
            const tempProducts = [...data]
            tempProducts[formData.key].productId = formData.productId
            tempProducts[formData.key].quantity = formData.quantity
            tempProducts[formData.key].price = formData.price
            setData(tempProducts)
            setFormData((prevFormData) => ({
                ...prevFormData,
                key: '',
                productId: '',
                quantity: '',
                price: ''
            }))
            setAction('Ajouter')
        }
    }
    const handleInputChange = (event) => {
        const { name, value } = event.target;
        setFormData((prevFormData) => ({
          ...prevFormData,
          [name]: value
        }));
    };

    const removeProduct=(index)=> {
        const tempProducts = [...data]
        tempProducts.splice(index, 1)
        setData(tempProducts)
    }

    const handleSend=()=> {
      const dataSend = {
        supplierId: formData.supplierId,
        proformaDetails: data
      }
      const requestOption = {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(dataSend)
      }
      fetch(API_CONFIG.SEND_PROFORMA, requestOption)
        .then(res => res.json())
        .then(res => {
          alert("Le proforma a bien été importé")
          console.log(res)
        })
        .catch((error) => {
          console.log(error)
          alert("Le proforma a bien été importé")
        });
    }

    useEffect(()=>{
      fetch(API_CONFIG.SUPPLIERS)
      .then(res=>res.json())
      .then(res=>{
        console.log(res.data)
        setSuppliers(res.data)
      })
      .catch(error=>console.log(error))
    },[])

    const handleSupplierChange=(event)=>{
      setFormData((prevFormData) => ({
        ...prevFormData,
        supplierId: event.target.value
      }));
      const dictSuppliers = toDictSupplier(suppliers)
      setProducts(dictSuppliers[event.target.value].supplierProducts)
    }

    return (
        <>
        <CRow>
            <CCol xs={12}>
                <CCard className="mb-4">
                    <CCardBody>
                        <CFormSelect onChange={handleSupplierChange} name='supplierId'>
                          <option>Choisir un fournisseur</option>
                            {suppliers.map((value, index) =>
                            <option value={value.supplierId} key={index}>{value.name}</option>
                            )}
                        </CFormSelect>
                    </CCardBody>
                </CCard>
            </CCol>
        </CRow>
            <CRow>
            <CCol xs={4}>
                <CCard className="mb-4">
                    <CCardBody>
                        <CCardTitle>Ajout de produit</CCardTitle>
                        <AddProformaForm addProduct={addProduct} formData={formData} handleInputChange={handleInputChange} products={products}/>
                    </CCardBody>
                </CCard>
            </CCol>
            <CCol xs={8}>
                <CCard className="mb-4">
                    <CCardBody>
                      <CRow>
                        <CCol>
                          <CCardTitle>Listes des produits dans le proforma</CCardTitle>
                        </CCol>
                        <CCol>
                          <CCardTitle className="text-end">
                            <CButton onClick={handleSend}>Validé</CButton>
                          </CCardTitle>
                        </CCol>
                      </CRow>
                        <AddProformaTable data={data} updateProduct={updateProduct} removeProduct={removeProduct}/>
                    </CCardBody>
                </CCard>
            </CCol>
        </CRow>
        </>

    )

}
export default AddProforma
