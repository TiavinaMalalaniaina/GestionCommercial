import CIcon from "@coreui/icons-react"
import { CButton, CModal, CModalBody, CModalFooter, CModalHeader, CModalTitle } from "@coreui/react"
import { useState } from "react"
import { cilPlus } from '@coreui/icons'

const AddProformaModal = () => {

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

    const [visible, setVisible] = useState(false)
    return (
    <>
        <CButton onClick={() => setVisible(!visible)}><CIcon icon={cilPlus} /> Choisir le founisseur </CButton>
        <CModal
        visible={visible}
        onClose={() => setVisible(false)}
        aria-labelledby="ScrollingLongContentExampleLabel"
        >
        <CModalHeader>
            <CModalTitle id="ScrollingLongContentExampleLabel">Choix du fournisseur</CModalTitle>
        </CModalHeader>
        <CModalBody>
            
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
export default AddProformaModal