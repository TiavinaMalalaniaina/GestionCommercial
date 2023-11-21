import {
    CButton,
    CCard,
  CCardBody,
  CCardTitle,
  CCol,
  CRow
} from '@coreui/react'
import { useState } from "react"
import API_CONFIG from 'src/apiconfig'
import { toDictRequest } from 'src/utils/dict'
import RequestProductValidationTable from '../tables/RequestProductValidationTable'



const RequestProductValidation = ({data, removeData, index}) => {
    const [validated, setValidated] = useState([])
    const updateValidated = (requestDetailsId,requestId) => {
        const existingRequest = validated.find(item => item.requestId === requestId);
        if (existingRequest) {
            const index = existingRequest.requestDetailsId.indexOf(requestDetailsId);
            if (index !== -1) {
                existingRequest.requestDetailsId.splice(index, 1);
            } else {
                existingRequest.requestDetailsId.push(requestDetailsId);
            }
        } else {
            const newRequest = {
                requestId: requestId,
                requestDetailsId: []
            };
            console.log(newRequest)
            setValidated([...validated, newRequest]);
        }
        console.log(validated)
    }

    const sendValidation=(requestId, index)=> {
        const requestOption={
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(toDictRequest(validated)[requestId])
        }
        fetch(API_CONFIG.VALIDATE_REQUEST, requestOption)
        .then(res => res.json())
        .then(res => alert("La demande a été validé avec succés"))
        .catch(error => {
          console.log(error)
          alert("La demande a été validé avec succés")
        })
        removeData(index)
    }

    return (
        <>
            <CRow>
                <CCol xs={12}>
                    <CCard className="mb-4">
                        <CCardBody>
                            <CRow>
                                <CCol>
                                    <CCardTitle>Demande du <strong>{data.createdAt}</strong> par <strong>Tiavina</strong></CCardTitle>
                                </CCol>
                                <CCol>
                                    <CCardTitle className='text-end'>
                                        <CButton onClick={()=>sendValidation(data.requestId, index)}>Validé</CButton>
                                    </CCardTitle>
                                </CCol>
                            </CRow>
                            <RequestProductValidationTable data={data.requestDetails} updateValidated={updateValidated}/>
                        </CCardBody>
                    </CCard>
                </CCol>
            </CRow>
        </>
    )
}
export default RequestProductValidation
