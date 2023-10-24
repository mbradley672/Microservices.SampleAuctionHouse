import React from 'react'
import {getSession, getTokenWorkaround} from "@/app/actions/authActions";
import Heading from "@/app/components/Heading";
import AuthTest from "@/app/session/AuthTest";

export default async function Session() {
    const session = await getSession();
    const token = await getTokenWorkaround();
    
    return (
        <>
            <Heading title={'Session Dashboard'} />   
            <div className={'bg-blue-200 border-2 border-blue-500'}>
                <h3 className={'text-lg'}>Session Data</h3>
                <pre>{JSON.stringify(session, null, 2)}</pre>
            </div>
            <div className={'mt-4'}>
                <AuthTest />
            </div>
            <div className={'bg-green-200 border-2 border-green-500'}>
                <h3 className={'text-lg'}>Token Data</h3>
                <pre className={'overflow-auto'}>{JSON.stringify(token, null, 2)}</pre>
            </div>
        </>
    )
}