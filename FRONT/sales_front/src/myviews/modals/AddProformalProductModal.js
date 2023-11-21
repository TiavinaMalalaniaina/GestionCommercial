import CIcon from "@coreui/icons-react"
import { CButton, CForm, CFormInput, CFormSelect, CInputGroup, CInputGroupText, CModal, CModalBody, CModalFooter, CModalHeader, CModalTitle } from "@coreui/react"
import { useState } from "react"
import { cilPlus, cilSearch } from '@coreui/icons'

const AddProformaProductModal = () => {

    const [products, setProducts] = useState([
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
        }
    ])

    const [visible, setVisible] = useState(false)
    return (
    <>
        <CButton onClick={() => setVisible(!visible)}><CIcon icon={cilPlus} /> Choisir l'article à ajouter </CButton>
        <CModal
        visible={visible}
        onClose={() => setVisible(false)}
        aria-labelledby="ScrollingLongContentExampleLabel"
        >
        <CModalHeader>
            <CModalTitle>
                Choix d'un article
            </CModalTitle>
        </CModalHeader>
        <CModalBody>
            <CFormSelect>
                <option>Produit</option>
                {products.map((product, index) => 
                <option key={index} value={product.productId}>{product.productName}</option>
                )}
            </CFormSelect>
        </CModalBody>
        <CModalFooter>
            <CButton color="secondary" onClick={() => setVisible(false)}>
            Close
            </CButton>
            <CButton color="primary">Save changes</CButton>
        </CModalFooter>
        </CModal>
    </>
    )

}
export default AddProformaProductModal