import RequestProductForm from "../forms/RequestProductForm"
import {
    CButton,
  CCard,
  CCardBody,
  CCardTitle,
  CCol,
  CRow
} from '@coreui/react'
import RequestProductTable from "../tables/RequestProductTable"
import { useEffect, useState } from "react"
import API_CONFIG from './../../apiconfig'
import { toDictProduct } from "src/utils/dict"


const RequestProduct = () => {
    const [products, setProducts] = useState([])
    const [productDict, setProductDict] = useState(toDictProduct([]))

    const [data, setData] = useState([]);
    const [formData, setFormData] = useState({
        key: '',
        productId: '',
        quantity: '',
        reason: ''
    })
    const [action, setAction] = useState('Ajouter');

    const handleInputChange = (event) => {
        const { name, value } = event.target;
        setFormData((prevFormData) => ({
          ...prevFormData,
          [name]: value
        }));
    };

    const updateProduct=(index)=> {
        setFormData((prevFormData) => ({
            ...prevFormData,
            productId: data[index].productId,
            quantity: data[index].quantity,
            reason: data[index].reason,
            key: index
        }));
        setAction('Mettre à jour')
    }

    const addProduct=()=> {
        if (formData.key === '') {
            setData([...data, {productId:formData.productId, quantity:formData.quantity, reason:formData.reason}])
            setFormData((prevFormData) => ({
                ...prevFormData,
                key: '',
                productId: '',
                quantity: '',
                reason: ''
            }))
        } else {
            const tempProducts = [...data]
            tempProducts[formData.key].productId = formData.productId
            tempProducts[formData.key].quantity = formData.quantity
            tempProducts[formData.key].reason = formData.reason
            setData(tempProducts)
            setFormData((prevFormData) => ({
                ...prevFormData,
                key: '',
                productId: '',
                quantity: '',
                reason: ''
            }))
            setAction('Ajouter')
        }
    }

    const removeProduct=(index)=> {
        const tempProducts = [...data]
        tempProducts.splice(index, 1)
        setData(tempProducts)
    }

    const handleSend=()=> {
        const requestOption = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
        }
        fetch(API_CONFIG.SEND_REQUEST, requestOption)
            .then(res => res.json())
            .then(res => alert("Votre demande a bien été envoyé"))
            .catch((error) => alert("Votre demande a bien été envoyé"));
      setData([])
    }

    useEffect(() => {
      fetch(API_CONFIG.ALL_PRODUCT)
        .then(res => res.json())
        .then(res => {
          setProducts(res.data)
          setProductDict(toDictProduct(products))
        })
        .catch((error) => console.log(error));
    }, []);



    return (
        <>
            <CRow>
                <CCol xs={4}>
                    <CCard className="mb-4">
                        <CCardBody>
                            <CCardTitle>Ajout de produit</CCardTitle>
                            <RequestProductForm addProduct={addProduct} handleInputChange={handleInputChange} formData={formData} products={products}/>
                        </CCardBody>
                    </CCard>
                </CCol>
                <CCol xs={8}>
                    <CCard className="mb-4">
                        <CCardBody>
                            <CRow>
                                <CCol>
                                    <CCardTitle>Listes des produits à demander</CCardTitle>
                                </CCol>
                                <CCol>
                                    <CCardTitle className="text-end"><CButton onClick={handleSend}> Envoyer </CButton></CCardTitle>
                                </CCol>
                            </CRow>
                            <RequestProductTable data={data} updateProduct={updateProduct} removeProduct={removeProduct} allProducts={products} productDict={productDict}/>
                        </CCardBody>
                    </CCard>
                </CCol>
            </CRow>
        </>
    )
}
export default RequestProduct
