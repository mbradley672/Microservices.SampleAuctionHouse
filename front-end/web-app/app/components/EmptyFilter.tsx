import React from 'react'
import Heading from "@/app/components/Heading";
import {Button} from "flowbite-react";
import {useParamsStore} from "@/hooks/useParamsStore";

type Props = {
    title?: string,
    subtitle?: string,
    showReset?: boolean
}

export default ({
                    title = 'No Results',
                    subtitle = 'Please try changing your filters',
                    showReset = true
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
                </div>
            </div>
        </>
    )
}