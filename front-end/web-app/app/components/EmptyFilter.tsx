'use client'

import React from 'react'
import Heading from "@/app/components/Heading";
import {Button} from "flowbite-react";
import {useParamsStore} from "@/hooks/useParamsStore";
import {signIn} from "next-auth/react";

type Props = {
    title?: string,
    subtitle?: string,
    showReset?: boolean,
    showSignin?: boolean,
    callbackUrl?: string
}

export default ({
                    title = 'No Results',
                    subtitle = 'Please try changing your filters',
                    showReset,
                    showSignin,
                    callbackUrl
                }: Props) => {
    const reset = useParamsStore(st => st.reset)
    return (
        <>
            <div
                className={'h-[40vh] flex flex-col gap-2 justify-center items-center shadow-lg border-b-blue-500 border-2'}>
                <Heading title={title} subtitle={subtitle} center/>
                <div className={'mt-4'}>
                    {showReset && (
                        <Button outline onClick={reset}>Remove Filter</Button>
                    )}
                    {showSignin && (<Button outline onClick={() => signIn('id-server', {callbackUrl})}>Sign In</Button> )}
                </div>
            </div>
        </>
    )
}