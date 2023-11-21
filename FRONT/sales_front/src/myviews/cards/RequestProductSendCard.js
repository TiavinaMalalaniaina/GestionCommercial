import {
    CButton,
    CCard,
  CCardBody,
  CCardSubtitle,
  CCardTitle,
  CCol,
  CRow
} from '@coreui/react'
import { useState } from "react"
import RequestProductValidationTable from '../tables/RequestProductValidationTable'



const RequestProductSendCard = () => {
    const [data, setData] = useState(
        [
            {
                productId: 1,
                productName: 'P1',
                quantity: 500,
                motif: 'Je le veut, c\'est tout',
            },
            {
                productId: 1,
                productName: 'P1',
                quantity: 500,
                motif: 'Je le veut, c\'est tout',
            },
            {
                productId: 1,
                productName: 'P1',
                quantity: 500,
                motif: 'Je le veut, c\'est tout',
            },
            {
                productId: 1,
                productName: 'P1',
                quantity: 500,
                motif: 'Je le veut, c\'est tout',
            }
        ]
    )

    return (
        <>
            <CRow>
                <CCol xs={12}>
                    <CCard className="mb-4">
                        <CCardBody>
                            <CCardTitle>Envoyé le <strong>2022-01-01</strong></CCardTitle>
                            <CTable striped>
                            <CTableHead>
                                <CTableRow>
                                    <CTableHeaderCell scope="col">#</CTableHeaderCell>
                                    <CTableHeaderCell scope="col">Produit</CTableHeaderCell>
                                    <CTableHeaderCell scope="col">Quantité</CTableHeaderCell>
                                    <CTableHeaderCell scope="col">Motif</CTableHeaderCell>
                                    <CTableHeaderCell scope="col">Action</CTableHeaderCell>
                                </CTableRow>
                            </CTableHead>
                            <CTableBody>
                                {data.map((value, index) => 
                                <CTableRow key={index}>
                                    <CTableHeaderCell scope="row">{index+1}</CTableHeaderCell>
                                    <CTableDataCell>{value.productName}</CTableDataCell>
                                    <CTableDataCell>{value.quantity}</CTableDataCell>
                                    <CTableDataCell>{value.motif}</CTableDataCell>
                                    <CTableDataCell>
                                    <CButton color='success'>Validé</CButton>
                                    </CTableDataCell>
                                </CTableRow>
                                )}
                            </CTableBody>
                        </CTable>
                        </CCardBody>
                    </CCard>
                </CCol>
            </CRow>
        </>
    )
}
export default RequestProductSendCard
