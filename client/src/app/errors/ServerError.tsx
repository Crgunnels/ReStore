import { Container, Divider, Paper, Typography } from "@mui/material"
import { useHistory, useLocation } from "react-router-dom"

export default function ServerError() {
    const history = useHistory();
    const {state} = useLocation<any>();
    return(
        <Container component={Paper}>
            {state?.error ? (
                <>
                <Typography variant="h3" gutterBottom color='error'>{state.error.title}</Typography>
                <Divider></Divider>    
                <Typography>{state.error.detail} || 'internal sever error' </Typography>
                </>
            ) : (
            <Typography variant="h5" gutterBottom>Server error</Typography>
            )}
            <button onClick={()=> history.push('/catalog')} >Go back to the store </button>

        </Container>
    )
}