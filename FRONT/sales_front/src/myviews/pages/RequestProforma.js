import {
    CButton,
    CCard,
    CCardBody,
    CCardSubtitle,
    CCardTitle,
    CCol,
    CFormLabel,
    CFormSelect,
    CRow
} from '@coreui/react'
import { useState } from 'react'
import { toDictSupplier } from 'src/utils/dict'
import SupplierCard from '../cards/SupplierCard'
import RequestProformaForm from '../forms/RequestProformaForm'


const RequestProforma=()=> {
    const [supplier, setSupplier] = useState('')
    const [suppliers, setSuppliers] = useState([
        {
            supplierId: 1,
            name: 'Tiavina',
            contactPhone: '00.0212115',
            contactEmail: 'sdfdsfds@gmail.com',
            address: 'sdlkjeifizhfnio',
            products: [
                {
                    productId: 1,
                    productName: 'P1'
                },
                {
                    productId: 2,
                    productName: 'P2'
                },
                {
                    productId: 3,
                    productName: 'P3'
                },
                {
                    productId: 4,
                    productName: 'P4'
                },
            ]
        },
        {
            supplierId: 2,
            name: 'Malalaniaina',
            contactPhone: '00.0212115',
            contactEmail: 'sdfdsfds@gmail.com',
            address: 'sdlkjeifizhfnio',
            products: [
                {
                    productId: 1,
                    productName: 'P1'
                },
                {
                    productId: 2,
                    productName: 'P2'
                },
                {
                    productId: 3,
                    productName: 'P3'
                },
                {
                    productId: 4,
                    productName: 'P4'
                },
            ]
        }
    ])
    const dictSupplier = toDictSupplier(suppliers)

    const handleSupplierChange=(e)=>{
        const { value } = e.target
        setSupplier(dictSupplier[value])
    }


    return (
        <>
            <CRow>
                <CCol xs={7}>
                    <CCard className="mb-4">
                        <CCardBody>
                            <CCardTitle>Fournisseur</CCardTitle>
                            <div className="mb-3">
                                <CFormLabel htmlFor="request-form-product">Produit</CFormLabel>
                                <CFormSelect 
                                aria-label="Default select example" 
                                name='product' 
                                onChange={handleSupplierChange}
                                >
                                <option>Choisissez un fournisseur</option>
                                {suppliers.map((value, index)=>
                                    <option value={value.supplierId} key={index}>{value.name}</option>
                                )}
                                </CFormSelect>
                            </div>
                        </CCardBody>
                    </CCard>
                </CCol>
                <CCol xs={5}>
                    <SupplierCard supplier={supplier} />
                </CCol>
            </CRow>
            <RequestProformaForm supplier={supplier} />
                        
        </>
    )
}
export default RequestProforma